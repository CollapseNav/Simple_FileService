import { Platform } from '@angular/cdk/platform';
import { Component, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { TypeService } from './services/type.service';
import { BaseFile } from './table/table/fileinfo';
import { TableColumn } from './table/table/tablecolumn';
import { SelConfig } from './typesel/selconfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'UI';
  selconfig!: SelConfig;

  tc: TableColumn<BaseFile>[] = [
    { label: 'Id', valIndex: 'id', sort: true },
    { label: 'Account', valIndex: 'field1', sort: true },
    { label: 'Number', valIndex: 'field2', sort: true },
    { label: 'PWD', valIndex: 'field3', sort: true },
    { label: 'Date', valIndex: 'addTime', sort: true },
  ];
  constructor(public typeServ: TypeService, public platform: Platform) { }
  ngOnInit() {
    this.selconfig = {
      list: this.typeServ.getTypeList(),
      count: 0,
      selected: this.typeServ.getTypeList(),
    };
  }
  toggleEmit(item: MatSidenav, status: boolean) {
    switch (status) {
      case undefined: { item.toggle(); break; }
      case true: { if (item.opened) item.toggle(); break; }
      case false: { if (!item.opened) item.toggle(); break; }
    }
  }
}
