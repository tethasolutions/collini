import { JobModel } from './job.model';
import { NoteModel } from './note.model';
import { UserModel } from './user.model';
import { ActivityStatusEnum } from '../enums/activity-status.enum';

export class ActivityModel {
    id: number;
    description: string;
    start: Date;
    end: Date;
    status: ActivityStatusEnum;
    statusChangedOn: Date;
    operatorId: number;
    jobId: number;
    notes: NoteModel[];
    customerName: string;
    jobCode: string;
    jobDescription: string;
    hasNotes: boolean;

    get expired(): boolean {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const expiration = new Date(this.end);
        expiration.setHours(0, 0, 0, 0);

        return today > expiration;
    }

    constructor() {
        this.id = null;
        this.description = " ";
        this.start = new Date(new Date().setMinutes(0));
        this.end = new Date(this.start.getTime() + (1000 * 60 * 60));
        this.status = ActivityStatusEnum.Planned;
        this.statusChangedOn = new Date();
        this.operatorId = null;
        this.jobId = null;
        this.notes = [];
    }
}
