import {UUID} from 'angular2-uuid';

export class Entity {
    id: string = UUID.UUID();
    name = '';
}
