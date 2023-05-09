export class AddressModel {
    addressId: number;
    customerId: number;
    city: string;
    address: string;
    province: string;
    zipCode: string;
    mainAddress: boolean;

    constructor() {
        this.addressId = null;
        this.customerId = null;
        this.city = null;
        this.address = null;
        this.province = null;
        this.zipCode = null;
        this.mainAddress = false;
    }
}
