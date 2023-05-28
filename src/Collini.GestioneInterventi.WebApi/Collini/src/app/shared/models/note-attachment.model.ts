import { NoteModel } from './note.model';

export class NoteAttachmentModel {
    displayName: string;
    fileName: string;
    noteId: number;
    note: NoteModel;
}