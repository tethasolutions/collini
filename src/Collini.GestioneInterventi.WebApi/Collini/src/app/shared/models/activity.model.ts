import { JobModel } from './job.model';
import { NoteModel } from './note.model';
import { UserModel } from './user.model';
import { ActivityStatusEnum } from '../enums/activity-status.enum';

export class ActivityModel {
    description: string;
    start: Date;
    end: Date;
    status: ActivityStatusEnum;
    statusChangedOn: Date;
    operatorId: number;
    operator: UserModel;
    jobId: number;
    job: JobModel;
    notes: NoteModel[];
}
