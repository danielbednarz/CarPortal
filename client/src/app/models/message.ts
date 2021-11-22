export interface Message {
    id: string;
    senderId: number;
    senderUsername: string;
    senderBrandId: number;
    senderModelId: number;
    recipientId: number;
    recipientUsername: string;
    recipientBrandId: number;
    recipientModelId: number;
    content: string;
    messageReadDate?: Date;
    messageSentDate: Date;
}