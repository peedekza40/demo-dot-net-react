import SvgColor from 'src/components/svg-color';

// ----------------------------------------------------------------------

const icon = (name: string) => (
  <SvgColor src={`/assets/icons/navbar/${name}.svg`} sx={{ width: 1, height: 1 }} />
);

const navConfig = [
  {
    title: 'home',
    path: '/',
    icon: icon('ic_analytics'),
    isDisplay: true
  },
  {
    title: 'user',
    path: '/user',
    icon: icon('ic_user'),
    isDisplay: true
  },
{
    title: 'role',
    path: '/role',
    icon: icon('ic_user'),
    isDisplay: true
},
  {
    title: 'product',
    path: '/products',
    icon: icon('ic_cart'),
    isDisplay: true
  },
  {
    title: 'blog',
    path: '/blog',
    icon: icon('ic_blog'),
    isDisplay: true
  },
];

export default navConfig;
