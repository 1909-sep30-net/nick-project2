import UserCreate from './user-create';

export default interface User extends UserCreate {
  id: number;
  admin: boolean;
}
