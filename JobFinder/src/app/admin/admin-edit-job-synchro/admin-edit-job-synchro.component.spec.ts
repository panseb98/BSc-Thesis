import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEditJobSynchroComponent } from './admin-edit-job-synchro.component';

describe('AdminEditJobSynchroComponent', () => {
  let component: AdminEditJobSynchroComponent;
  let fixture: ComponentFixture<AdminEditJobSynchroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminEditJobSynchroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminEditJobSynchroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
