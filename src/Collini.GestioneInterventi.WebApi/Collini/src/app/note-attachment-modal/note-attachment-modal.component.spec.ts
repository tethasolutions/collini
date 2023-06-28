import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteAttachmentModalComponent } from './note-attachment-modal.component';

describe('NoteAttachmentModalComponent', () => {
  let component: NoteAttachmentModalComponent;
  let fixture: ComponentFixture<NoteAttachmentModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoteAttachmentModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NoteAttachmentModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
