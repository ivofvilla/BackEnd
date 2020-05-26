#################### api ceps #########################
from acesso_cep import BuscaEndereco

cep = "07111000"
objeto = BuscaEndereco(cep)

logradroudo, bairro, localidade, uf =objeto.acesso_via_cep()

print(logradroudo, bairro, localidade, uf)

######################## datas ##################

#from datas_br import  DatasBr

#cadastro = DatasBr()
#print(cadastro.mes_cadastro())
#print(cadastro.dia_semana())
#print(cadastro.tempo_cadastro())


############## regex telefone ####################

#from Telefones import TelefonesBr

#telefone = "05511976237488"

#telefone_objeto = TelefonesBr(telefone)
#print(telefone_objeto)

#padrao_molde = "(xx)aaaa-wwww"
#padrao = "[0-9]{2}[0-9]{4,5}[0-9]{4}"
#texto = "meu telefone eh 11912344566 e o telefone 11925251147 eh de um conhecido"

#resposta = re.findall(padrao, texto)
#print(resposta)

### regex email ###

#padrao = "\w{5,50}@\w{3,10}.\w{2,3}.\w{2,3}"

#texto = "khasdjfsjdflasdfjkasjdflasdf ivofvilla@hotmail.com hihwhefkandnfkldfnsldf kjsfkjdhskjdfhglshdfgk"
#resposta = re.search(padrao, texto)

#print(resposta.group())

##################### CPF e CNPJ ############################
#from Cpf_Cnpj import Documento

#cpf = 31737596806
#cnpj = 20221421000150
#objeto_cpf = Documento.cria_documento(cpf)

#objeto_cnpj = Documento.cria_documento(cnpj)

#print(objeto_cpf)
#print(objeto_cnpj)

