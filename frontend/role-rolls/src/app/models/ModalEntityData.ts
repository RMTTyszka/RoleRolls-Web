import { Entity } from './Entity.model';

export interface ModalEntityData<T extends Entity> {
  entity: T;
  action: EditorAction;
}

export enum EditorAction {
  create,
  update
}
