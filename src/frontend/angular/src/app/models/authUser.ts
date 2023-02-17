import { User } from './user';

export interface AuthUser {
    token: string;
    user: User;
}
