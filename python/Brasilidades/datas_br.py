from datetime import datetime, timedelta

class DatasBr:
    def __init__(self):
        self.momento_cadastro = datetime.today()

    def __str__(self):
        return self.format_data()

    def mes_cadastro(self):
        meses_ano = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro",
                     "Outubro", "Novembro", "Dezemrbo"]

        return meses_ano[self.momento_cadastro.month-1]

    def ano_cadastro(self):
        return self.momento_cadastro.year

    def dia_semana(self):
        semana = ["Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sabado", "Domingo"]
        return semana[self.momento_cadastro.weekday()]

    def format_data(self):
        return self.momento_cadastro.strftime("%d/%m/%Y %H:%M")

    def tempo_cadastro(self):
        return (datetime.today() + timedelta(days=30)) - self.momento_cadastro