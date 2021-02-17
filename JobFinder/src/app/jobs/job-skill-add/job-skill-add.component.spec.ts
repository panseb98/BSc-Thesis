import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobSkillAddComponent } from './job-skill-add.component';

describe('JobSkillAddComponent', () => {
  let component: JobSkillAddComponent;
  let fixture: ComponentFixture<JobSkillAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobSkillAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobSkillAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
