import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpericenceComponent } from './expericence.component';

describe('ExpericenceComponent', () => {
  let component: ExpericenceComponent;
  let fixture: ComponentFixture<ExpericenceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpericenceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpericenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
