import { Component, OnInit, Input } from '@angular/core';
import { Reels } from 'src/app/_models/Reels';

@Component({
  selector: 'app-reel-card-detail',
  templateUrl: './reel-card-detail.component.html',
  styleUrls: ['./reel-card-detail.component.css']
})
export class ReelCardDetailComponent implements OnInit {
  @Input() reel: Reels;
  constructor() { }

  ngOnInit(): void {
  }

  

}
