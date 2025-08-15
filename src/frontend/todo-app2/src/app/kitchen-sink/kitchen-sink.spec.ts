import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KitchenSink } from './kitchen-sink';

describe('KitchenSink', () => {
  let component: KitchenSink;
  let fixture: ComponentFixture<KitchenSink>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KitchenSink]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KitchenSink);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
