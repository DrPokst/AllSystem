import { Component, OnInit } from '@angular/core';
import { ReelService } from 'src/app/_services/reel.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-reelregister',
  templateUrl: './reelregister.component.html',
  styleUrls: ['./reelregister.component.css']
})
export class ReelregisterComponent implements OnInit {
  model: any = {};
  public imagePath;
  imageURL: string;
  url: any;
  registerForm: FormGroup;
  
  constructor(private reelService: ReelService, private  alertify: AlertifyService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup(
      {
        CMnf: new FormControl(),
        QTY: new FormControl(),
        file: new FormControl('', [Validators.required]),
        fileSource: new FormControl('', [Validators.required])
      }
    );
  }

  onFileChange(event) {

    if (event.target.files.length > 0) {

      const file = event.target.files[0];

      this.registerForm.patchValue({

        fileSource: file

      });
    }
  }
  showPreview(event) {
    const file = (event.target as HTMLInputElement).files[0];
    this.registerForm.patchValue({
      fileSource: file
    });
    this.registerForm.get('fileSource').updateValueAndValidity()

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  submit(){

    const formData = new FormData();

    formData.append('file', this.registerForm.get('fileSource').value);
    formData.append('CMnf', this.registerForm.get('CMnf').value);
    formData.append('QTY', this.registerForm.get('QTY').value);

    this.model = Object.assign({}, this.registerForm.value);


    this.reelService.registerReel(formData). subscribe(()=>{
      this.alertify.success('sekmingai uzregistruota');
    }, error => {
      this.alertify.error(error);
    });
    
    console.log(formData);
    this.registerForm.reset();
    this.imageURL = null;
  }

  

}
