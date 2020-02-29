import {Entity} from './Entity.model';

export interface BaseCrudResponse<T extends Entity> {
  success: boolean;
  message: string;
  entity: T;
}
