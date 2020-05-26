from flask import render_template, request, redirect, session, flash, url_for, send_from_directory
from models import Jogo
from dao import JogoDao, UsuarioDao
import time
from helpers import deleta_arquivo, recupera_imagem
from jogoteca import  db, app

db = MySQL(app)

jogo_dao = JogoDao(db)
usuario_dao = UsuarioDao(db)

@app.route('/')
def index():
    lista = jogo_dao.listar()
    return render_template('lista.html', titulo='Novo jogo', jogos=lista)

@app.route('/criar', methods=['POST',])
def criar():
    nome = request.form['nome']
    categoria = request.form['categoria']
    console = request.form['console'] #name do html
    novoJogo = Jogo(nome, categoria, console)
    jogo = jogo_dao.salvar(novoJogo)

    timestamp = time.time()

    img = request.files['arquivo']
    img.save('{}/capa{}-{}.jpg'.format(app.config['UPLOAD_PATH'], jogo.id, timestamp))

    return redirect(url_for('index'))

@app.route('/novo')
def novo():
    if 'usuario_logado' not in session or session['usuario_logado'] == None:
        pagina = url_for('novo')
        return redirect(url_for('login', proxima=pagina))
    return render_template('novo.html', titulo='Novo jogo')

@app.route('/editar/<int:id>')
def editar(id):
    if 'usuario_logado' not in session or session['usuario_logado'] == None:
        pagina = url_for('editar')
        return redirect(url_for('login', proxima=pagina))

    nome_img = recupera_imagem(id)
    timestamp = time.time()
    jogo = jogo_dao.busca_por_id(id)
    capa = 'capa{}-{}.jpg'.format(jogo.id, timestamp)
    return render_template('editar.html', titulo='Editando jogo', jogo=jogo, capa_jogo=capa)

@app.route('/deletar/<int:id>')
def deletar(id):
    jogo_dao.deletar(id)
    flash('Jogo Removido com sucesso!')

    return render_template(url_for('index'))

@app.route('/atualizar', methods=['POST',])
def atualizar():
    atualiza_jogo = jogo_dao.busca_por_id(request.form['id'])
    atualiza_jogo.nome = request.form['nome']
    atualiza_jogo.categoria = request.form['categoria']
    atualiza_jogo.console = request.form['console']
    jogo_dao.salvar(atualiza_jogo)

    arquivo = request.files['arquivo']
    upload_path = app.config['UPLOAD_PATH']
    timestamp = time.time()
    deleta_arquivo(atualiza_jogo.id)
    arquivo.save(f'{upload_path}/capa{atualiza_jogo.id}={timestamp}.jpg')

    return redirect(url_for('index'))

@app.route('/login')
def login():
    proximo = request.args.get('proxima')
    return render_template('login.html', proxima=proximo)

@app.route('/autenticar', methods=['POST',])
def autenticar():
    usuario = usuario_dao.buscar_por_id(request.form['usuario'])
    senha = request.form['senha']

    if usuario:
        if usuario.senha == senha:
            session['usuario_logado'] = usuario.id
            flash(usuario.nome + ' usuário logou com sucesso ')
            proxima_pagina = request.form['proxima']
            return redirect(url_for('novo', proxima=proxima_pagina))
    else:
        flash('usuário ou senha inválida!')
        return url_for('login')

@app.route('/logout')
def logout():
    session['usuario_logado'] = None
    flash('Nenhum usuário logado')
    return redirect(url_for('index'))

@app.route('/uploads/<filename>')
def imagem(filename):
    return send_from_directory('uploads', filename)
