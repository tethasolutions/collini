export class CopyActivityModel {
    id: number;
    newOperatorId: number;
    start: Date;
    end: Date;

    constructor() {
        this.id = null;
        this.newOperatorId = null;
        this.start = new Date(new Date().getFullYear(),new Date().getMonth(),new Date().getDay(),new Date().getHours(),0);
        this.end = new Date(this.start.getTime() + (1000 * 60 * 60));
    }
}
