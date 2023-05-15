export class AddressModel {
    addressId: number;
    customerId: number;
    city: string;
    address: string;
    province: string;
    zipCode: string;
    isMainAddress: boolean;

    get fullAddress(): string {
        let result = '';
        if (this.address !== null) { result += `${this.address}, `; }
        if (this.city !== null) { result += `${this.city}, `; }
        if (this.province !== null) { result += `${this.province}, `; }
        if (this.zipCode !== null) { result += `${this.zipCode}`; }
        return result;
    }

    constructor() {
        this.addressId = null;
        this.customerId = null;
        this.city = null;
        this.address = null;
        this.province = null;
        this.zipCode = null;
        this.isMainAddress = false;
    }
}
