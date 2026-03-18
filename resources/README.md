# Whiteboard em tempo real — Angular + Konva.js + SignalR

## Estrutura dos arquivos

```
src/app/
├── services/
│   └── drawing-hub.service.ts    ← Serviço Angular que gerencia a conexão SignalR
├── components/
│   └── whiteboard.component.ts   ← Componente Konva com canvas + tokens

backend/
└── DrawingHub.cs                 ← Hub SignalR + interface do repositório (.NET)
```

## Instalação Angular

```bash
npm install konva @microsoft/signalr
npm install --save-dev @types/konva
```

## Configuração no app.config.ts

```typescript
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    // ... outros providers
  ]
};
```

## Uso no template

```html
<!-- app.component.html -->
<app-whiteboard />
```

## Backend (.NET)

Adicione no `Program.cs`:

```csharp
builder.Services.AddSignalR();
builder.Services.AddScoped<IDrawingRepository, DrawingRepository>(); // sua implementação

app.MapHub<DrawingHub>("/hubs/drawing");
```

## Fluxo de dados

```
Usuário desenha / arrasta token
        ↓
Konva captura o evento (mousemove / dragend)
        ↓
throttleTime(50ms) via RxJS — limita a 20 eventos/segundo
        ↓
DrawingHubService.sendStroke() / sendTokenMoved()
        ↓
SignalR → DrawingHub.cs → _repo.SaveAsync() → banco
        ↓
Clients.OthersInGroup().SendAsync("ReceiveStroke" / "ReceiveTokenMoved")
        ↓
Outros usuários recebem via hub.remoteStroke$ / remoteTokenMoved$
        ↓
Konva renderiza na tela dos outros
```

## Funcionalidades implementadas

- ✅ Desenho livre com caneta (Konva.Line com tensão suave)
- ✅ Tokens arrastáveis (Konva.Group com draggable: true)
- ✅ Sincronização em tempo real via SignalR
- ✅ Throttle de 50ms no drag para não inundar o hub
- ✅ Duas layers separadas (desenho + tokens) para melhor performance
- ✅ Toolbar: trocar ferramenta, cor, espessura
- ✅ Carregamento do estado do canvas ao entrar na sala
- ✅ Suporte a salas (grupos SignalR via roomId)
- ✅ Tokens de outros usuários não são arrastáveis localmente

## Próximos passos sugeridos

- Adicionar imagem de mapa como fundo (Konva.Image na layer de desenho)
- Tokens com imagem (avatar de personagem RPG, por exemplo)
- Zoom e pan do stage (Konva.Stage.scale + drag no stage)
- Undo/Redo (pilha de strokes no component)
- Cursor de outros usuários em tempo real (envia posição do mouse)
