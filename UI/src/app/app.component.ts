import { Component, OnInit } from '@angular/core';
import { TypeService } from './services/type.service';
import { SelConfig } from './typesel/selconfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'UI';
  selconfig!: SelConfig;
  constructor(public typeServ: TypeService) { }
  ngOnInit() {
    this.selconfig = {
      list: this.typeServ.getTypeList(),
      count: 0,
      selected: this.typeServ.getTypeList(),
    };
  }
  testEmit(data: any) {
    console.log(data);
    console.log(this.selconfig);
  }
}
