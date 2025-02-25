import { Entity } from './Entity.model';

export interface EntityActionData<T extends Entity> {
  entity: T;
  action: EditorAction;
}

export enum EditorAction {
  create,
  update,
  delete,
}
