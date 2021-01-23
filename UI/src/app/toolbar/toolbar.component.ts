import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.sass']
})
export class ToolbarComponent implements OnInit {

  // @Input() collapse;
  @Output() collapse = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  sidernavToggle() {
    this.collapse.emit();
  }

}
