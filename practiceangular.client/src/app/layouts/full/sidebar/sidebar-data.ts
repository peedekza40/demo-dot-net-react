import { NavItem } from './nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Home',
  },
  {
    displayName: 'Home',
    iconName: 'layout-dashboard',
    route: '/home',
  },
  {
    navCap: 'Auth',
  },
  {
    displayName: 'Login',
    iconName: 'lock',
    route: '/auth/login',
  },
  {
    displayName: 'Register',
    iconName: 'user-plus',
    route: '/auth/register',
  },
];
