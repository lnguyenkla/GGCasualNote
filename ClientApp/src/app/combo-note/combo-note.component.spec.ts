import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComboNoteComponent } from './combo-note.component';

describe('ComboNoteComponent', () => {
  let component: ComboNoteComponent;
  let fixture: ComponentFixture<ComboNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComboNoteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComboNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
