import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserJobTitlesComponent } from './user-job-titles.component';

describe('UserJobTitlesComponent', () => {
  let component: UserJobTitlesComponent;
  let fixture: ComponentFixture<UserJobTitlesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserJobTitlesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserJobTitlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
