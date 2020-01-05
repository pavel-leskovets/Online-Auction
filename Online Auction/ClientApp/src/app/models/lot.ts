import { Bid } from './bid';

export class Lot {
    id: number;
    name: string;
    initialPrice: number;
    currentPrice: number;
    categoryId: number;
    beginDate: Date;
    endDate: Date;
    userId: number;
    description: string;
    image: any;
    imageUrl: any;
    bids: Bid[];
    
}
