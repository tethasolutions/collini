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

    constructor() {
        this.id = null;
        this.description = null;
        this.start = new Date();
        this.end = new Date(this.start.getTime() + (1000 * 60 * 60));
        this.status = ActivityStatusEnum.Planned;
        this.statusChangedOn = new Date();
        this.operatorId = null;
        this.jobId = null;
        this.notes = [];
    }
}
