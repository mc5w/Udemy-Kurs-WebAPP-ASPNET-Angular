import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent {

  constructor(private router: Router) {
    
    
  }
  public goToBildungsprogramm(){
    this.router.navigateByUrl('/');
  }

}
