import { UserService } from 'src/app/services/user.service';
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";

@Injectable({
  providedIn :"root"
})

export class AdminService implements CanActivate{

  constructor(private userService:UserService,
              private router:Router){

  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    boolean {
      if(!this.userService.isLoggedIn()){
        this.router.navigateByUrl('/auth-login');
        return false;
      }
      return true;
  }

}
