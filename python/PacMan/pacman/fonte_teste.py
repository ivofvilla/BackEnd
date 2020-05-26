import pygame

pygame.init()

tela = pygame.display.set_mode((800, 600),0)
pontos = 10
texto = "Pontuação {}".format(pontos)

fonte = pygame.font.SysFont("arial", 48, True, False)
img_texto = fonte.render(texto, True, (255,255,0))

while True:

    tela.blit(img_texto, (100,100))
    pygame.display.update()

    for e in pygame.event.get():
        if e.type == pygame.QUIT:
            exit()

