import { Component, Injectable, OnInit } from '@angular/core';

@Component({
  selector: 'app-member-statistics',
  templateUrl: './member-statistics.component.html',
  styleUrls: ['./member-statistics.component.css']
})
export class MemberStatisticsComponent implements OnInit {
  dataSource: Data[];


  constructor() { 
    this.dataSource = data;
  }

  ngOnInit(): void {
  }

}

export class Data {
  day: string;
  oranges: number;
}

let data: Data[] = [{
  day: "Monday",
  oranges: 3
}, {
  day: "Tuesday",
  oranges: 2
}, {
  day: "Wednesday",
  oranges: 3
}, {
  day: "Thursday",
  oranges: 4
}, {
  day: "Friday",
  oranges: 6
}, {
  day: "Saturday",
  oranges: 11
}, {
  day: "Sunday",
  oranges: 4
}];

    
