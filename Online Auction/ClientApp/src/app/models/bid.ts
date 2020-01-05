import { User } from './user';

export class Bid {
    id: number;
    bidPrice: number;
    userId: number;
    lotId: number;
    userName: string;
    bidDate: Date;
    appUser: User;
}
