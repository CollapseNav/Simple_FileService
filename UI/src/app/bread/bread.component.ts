import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CurrentpageService } from '../services/currentpage.service';
import { Dir } from '../table/table/fileinfo';

@Component({
  selector: 'app-bread',
  templateUrl: './bread.component.html',
  styleUrls: ['./bread.component.sass']
})
export class BreadComponent implements OnInit {

  @Output() changePage = new EventEmitter<Dir>();
  pageRoute: Dir[];
  constructor(public cur: CurrentpageService) { }

  ngOnInit(): void {
    this.pageRoute = this.cur.getPageRoute();
  };

  popTo(page: Dir) {
    this.pageRoute.splice(this.pageRoute.indexOf(page));
    this.changePage.emit(page);
  }
}
