export interface CreatureShopService {
  buy(creatureId: string, shopId: string, itemId: string, quantity: number);
}
