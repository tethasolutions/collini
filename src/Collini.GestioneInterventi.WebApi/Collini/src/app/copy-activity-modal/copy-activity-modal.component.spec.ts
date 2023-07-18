import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CopyActivityModalComponent } from './copy-activity-modal.component';

describe('ActivityModalComponent', () => {
  let component: CopyActivityModalComponent;
  let fixture: ComponentFixture<CopyActivityModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CopyActivityModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CopyActivityModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
