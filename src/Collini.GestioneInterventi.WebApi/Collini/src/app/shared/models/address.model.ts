import { v4 as uuidv4 } from 'uuid';

export class AddressModel {
    id: number;
    contactId: number;
    city: string;
    streetAddress: string;
    province: string;
    zipCode: string;
    telephone: string;
    email: string;
    isMainAddress: boolean;
    tempId: string;

    get fullAddress(): string {
        let result = '';
        if (this.streetAddress !== null) { result += `${this.streetAddress} - `; }
        if (this.city !== null) { result += `${this.city} `; }
        return result;
    }

    constructor() {
        this.id = null;
        this.contactId = null;
        this.city = null;
        this.streetAddress = null;
        this.province = null;
        this.zipCode = null;
        this.isMainAddress = false;
        this.tempId = uuidv4();
        this.telephone = null;
        this.email = null;
    }
}
