import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
// import { ImageService } from './image.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

interface ImageVersion {
  src: SafeResourceUrl;
  label: string;
  }
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

	})

export class AppComponent {
  title = 'ImageStyleCreatorUI';

  rowId : any;
  originalImage: any;
  imageVersions: ImageVersion[] = [];

  selectedFile: File | undefined;

  constructor(private http: HttpClient,private sanitizer: DomSanitizer) {}
	// constructor(private imageService: ImageService, private sanitizer: DomSanitizer) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  ngOninit()
  {
    this.originalImage=[]
    this.imageVersions= [];
  }

  uploadPhoto() {   
    if (this.selectedFile) {
      console.log("inside uploadPhoto ")

      const formData = new FormData();

      formData.append('photo', this.selectedFile, this.selectedFile.name);

      this.http.post('https://localhost:7145/api/Image/upload', formData)
        .subscribe((data:any) => {
      debugger           
            console.log(data.rowid);
            this.rowId=data.rowid;
            // Convert image bytes to Base64 data URLs using DomSanitizer
      this.originalImage = this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.originalBytes);
// Add other image versions to the imageVersions array
this.imageVersions.push({
  src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.thumbnailBytes),
  label: 'Thumbnail'
  });
  
  this.imageVersions.push({
  src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.webBytes),
  label: 'Web'
  });
  
  this.imageVersions.push({
  src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.tabletBytes),
  label: 'Tablet'
  });
  
  this.imageVersions.push({
  src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.mobileBytes),
  label: 'Mobile'
  });
      },
          (error) => {

            // Handle error
            console.error('Error:', error);
          }
        );
      }
  }

  UpdateOldImage(oldId : any)
  {
    debugger
    if (this.selectedFile) {
      const formData = new FormData();

      formData.append('photo', this.selectedFile, this.selectedFile.name);
      
      this.http.post('https://localhost:7145/api/Image/UpdateOldImage/'+ oldId,formData)
        .subscribe((data:any) => {
      debugger
      console.log(data);

      this.ngOninit();

      this.originalImage = this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.originalBytes);
      // Add other image versions to the imageVersions array
      this.imageVersions.push({
        src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.thumbnailBytes),
        label: 'Thumbnail'
        });
        
        this.imageVersions.push({
        src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.webBytes),
        label: 'Web'
        });
        
        this.imageVersions.push({
        src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.tabletBytes),
        label: 'Tablet'
        });
        
        this.imageVersions.push({
        src: this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + data.mobileBytes),
        label: 'Mobile'
        });

  })
}}

  DeleteImage(oldId : any)
 {
  debugger
   if (oldId>0) {
    console.log("inside if this.selectedfile ")
    this.http.post('https://localhost:7145/api/Image/DeleteImage/'+ oldId, oldId)
      .subscribe((data:any) => {
    debugger
    console.log(data);
    this.ngOninit();

})
}
  }
}
