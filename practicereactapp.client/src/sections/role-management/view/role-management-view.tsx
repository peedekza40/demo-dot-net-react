import React from 'react';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import MUIDataTable from "mui-datatables/dist";

import Iconify from 'src/components/iconify';

function RoleManagementView() {
  const columns = ["Name", "Company", "City", "State"];

  const data = [
  ["Joe James", "Test Corp", "Yonkers", "NY"],
  ["John Walsh", "Test Corp", "Hartford", "CT"],
  ["Bob Herm", "Test Corp", "Tampa", "FL"],
  ["James Houston", "Test Corp", "Dallas", "TX"],
  ];

  const options = {
    filterType: 'checkbox',
    download: false,
    print: false,
    viewColumns: false,
    caseSensitive: true,
    draggableColumns: {
      enabled: true
    },
    responsive: "standard",
    searchAlwaysOpen: true,
    searchPlaceholder: "Search role...",
    onTableChange: (action: any, tableState: any) => {
      console.log(action, tableState);
    }
  };

  return (
    <Container>
      <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
        <Typography variant="h4">Roles</Typography>

        <Button variant="contained" color="inherit" startIcon={<Iconify icon="eva:plus-fill" />}>
          New Role
        </Button>
      </Stack>

      <Card>
        <MUIDataTable
          sx={{ minWidth: 800 }}
          data={data}
          columns={columns}
          options={options}
        />
      </Card>
    </Container>
  );
}

export default RoleManagementView;