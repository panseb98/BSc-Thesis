import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobsSkillsListComponent } from './jobs-skills-list.component';

describe('JobsSkillsListComponent', () => {
  let component: JobsSkillsListComponent;
  let fixture: ComponentFixture<JobsSkillsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobsSkillsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobsSkillsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
