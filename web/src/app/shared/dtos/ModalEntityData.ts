import {Entity} from '../models/Entity.model';

export interface ModalEntityData<T extends Entity> {
  entity: T;
  action: ModalEntityAction;
}

export enum ModalEntityAction {
  create,
  update
}
