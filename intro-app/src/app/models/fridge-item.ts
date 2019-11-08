import FridgeItemCreate from './fridge-item-create';

export default interface FridgeItem extends FridgeItemCreate {
  id: number;
  expiration: Date;
  ownerId: number;
}
