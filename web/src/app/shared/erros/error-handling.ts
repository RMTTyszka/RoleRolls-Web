import {Message, MessageService} from 'primeng/api';

export function handleValidation(area: ErrorArea, error: string, messageService: MessageService) {
    messageService.add({severity: 'error', summary: ErrorMessages.message(area, error), key: 'mainToast'} as Message);
}


export enum ErrorArea {
  equipment,
  inventory
}

export class ErrorMessages {
  public static message(area: ErrorArea, error: string) {
    switch (area) {
      case ErrorArea.equipment:
        switch (error) {
          case 'Incompatibility':
            return 'Equipment cannot be equipe due incompability with other items';
          default: {
            return 'Not Possible';
          }
        }
      case ErrorArea.inventory:
        return 'Not Possible';
      default: {
        return 'Not Possible';
      }
    }
  }
}
