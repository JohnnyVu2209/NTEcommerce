import * as Yup from 'yup';
import { LoadingButton } from '@mui/lab';
import { useForm } from 'react-hook-form';
import { useEffect, useState } from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import { Card, Container, Stack, Typography } from '@mui/material';
import { Link as RouterLink, useNavigate } from 'react-router-dom';

import categoryService from '../../services/CategoryService';
import { FormProvider, RHFTextField } from '../../components/hook-form';
import Page from '../../components/Page';
import Iconify from '../../components/Iconify';

export default function CreateCategory() {
    const navigate = useNavigate();

  const [selectCategory, setSelectCategory] = useState([]);

  const CreateSchema = Yup.object().shape({
    name: Yup.string().required('Category name is required'),
  });

  useEffect(() => {
    categoryService.getListCategories().then((response) => {
      setSelectCategory(response.data);
    });
  }, []);

  const method = useForm({
    resolver: yupResolver(CreateSchema),
    defaultValues: {
      name: '',
    },
  });

  const {
    handleSubmit,
    formState: { isSubmitting },
  } = method;

  const onSubmit = async () => {
    navigate('/dashboard', { replace: true });
  };

  return (
    <Page title="Create category">
      <Container>
        <Card>
          <FormProvider methods={method} onSubmit={handleSubmit(onSubmit)}>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
              <Typography variant="h4" gutterBottom>
                Create Category
              </Typography>
              <LoadingButton
                component={RouterLink}
                type="submit"
                variant="contained"
                loading={isSubmitting}
                startIcon={<Iconify icon="eva:plus-fill" />}
              >
                New Category
              </LoadingButton>
            </Stack>
            <Stack spacing={3}>
              <RHFTextField name="name" label="Category name" />
            </Stack>
          </FormProvider>
        </Card>
      </Container>
    </Page>
  );
}
