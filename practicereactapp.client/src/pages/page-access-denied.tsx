import { Helmet } from 'react-helmet-async';

import { AccessDeniedView } from 'src/sections/error';

// ----------------------------------------------------------------------

export default function AccessDeniedPage() {
  return (
    <>
      <Helmet>
        <title> 403 Access Denied </title>
      </Helmet>

      <AccessDeniedView />
    </>
  );
}
