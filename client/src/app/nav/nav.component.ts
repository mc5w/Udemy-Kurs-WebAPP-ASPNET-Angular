import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model: any = {};
  // currentUser$: Observable<User | null> = of(null)

  constructor(public accountService: AccountService) {  }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe({ // führt einen HTTP Request aus. Dabei muss man nicht unsubscriben, da der Request sowieso abgeschlossen ist
      next: response => {
        console.log(response);
      },
      error: error => console.log(error)
    });
  }

  logout(){
    this.accountService.logout();
  }

    // getCurrentUser(){
  //   this.accountService.currentUser$.subscribe({
  //     next: user => this.loggedIn = !!user,               // !! = ist User vorhanden => true sonst false
  //     error: error => console.log(error)
  //   })
  // }
}
