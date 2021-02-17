import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobsTitleListComponent } from './jobs-title-list.component';

describe('JobsTitleListComponent', () => {
  let component: JobsTitleListComponent;
  let fixture: ComponentFixture<JobsTitleListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobsTitleListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobsTitleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
