import { Component, OnInit } from '@angular/core';
import { ReelService } from 'src/app/_services/reel.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-reelregister',
  templateUrl: './reelregister.component.html',
  styleUrls: ['./reelregister.component.css']
})
export class ReelregisterComponent implements OnInit {
  model: any = {};
  
  constructor(private reelService: ReelService, private  alertify: AlertifyService) { }

  ngOnInit(): void {
  }

  registerreel(){
    this.reelService.registerReel(this.model).subscribe(()=>{
      this.alertify.success('sekmingai uzregistruota');
    }, error => {
      this.alertify.error(error);
    });
    console.log(this.model);
  }

}
