import { Component, OnInit } from '@angular/core';
import { Reels } from 'src/app/_models/Reels';
import { ReelService } from 'src/app/_services/reel.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Components } from 'src/app/_models/components';
import { ComponentService } from 'src/app/_services/component.service';

@Component({
  selector: 'app-reel-detail',
  templateUrl: './reel-detail.component.html',
  styleUrls: ['./reel-detail.component.css']
})
export class ReelDetailComponent implements OnInit {
  reels: Reels;
  components: Components;
  constructor(private componentService: ComponentService, private reelService: ReelService, private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.loadReel();
    this.loadComponent();
    
  }

  loadReel(){
    this.reelService.getReel(this.route.snapshot.params['id']).subscribe((reels: Reels) =>{
      this.reels = reels;
    }, error => {
      this.alertify.error(error);
    });
  }

  loadComponent() {
    this.componentService
      .getComponentMnf(this.reels.cMnf)
      .subscribe(
        (components: Components) => {
          this.components = components;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }


}
