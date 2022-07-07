import * as Yup from 'yup';
import ReactQuill from 'react-quill';
import { toast } from 'react-toastify';
import 'react-quill/dist/quill.snow.css';
import { LoadingButton } from '@mui/lab';
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { useEffect, useState } from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Card, Container, FormControl, InputLabel, MenuItem, Select, Stack, Typography } from '@mui/material';

import Page from '../../components/Page';
import Iconify from '../../components/Iconify';
import { ErrorCode } from '../../constant/ErrorCode';
import { SuccessCode } from '../../constant/SuccessCode';
import categoryService from '../../services/CategoryService';
import { createCategory } from '../../features/categorySlice';
import { FormProvider, RHFTextField } from '../../components/hook-form';

const toolbarOptions = [
  ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
  ['blockquote', 'code-block'],

  [{ 'header': 1 }, { 'header': 2 }],               // custom button values
  [{ 'list': 'ordered' }, { 'list': 'bullet' }],
  [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
  [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
  [{ 'direction': 'rtl' }],                         // text direction

  [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
  [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

  [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
  [{ 'font': ['sans-serif', 'serif'] }],
  [{ 'align': [] }],
  ['link', 'image'],

  ['clean']                                         // remove formatting button
];
export default function CreateCategory() {
  const navigate = useNavigate();
  
  const dispatch = useDispatch();

  const [selectCategory, setSelectCategory] = useState([]);

  const CreateSchema = Yup.object().shape({
    name: Yup.string().required('Category name is required'),
  });

  useEffect(() => {
    categoryService.getListCategories().then((response) => {
      setSelectCategory(response.data);
    });
  }, []);

  const methods = useForm({
    resolver: yupResolver(CreateSchema),
    defaultValues: {
      name: '',
      parentId: null,
      description: '',
    },
  });

  const {
    handleSubmit,
    formState: { isSubmitting },
    register,
    watch,
    setValue
  } = methods;


  const onSubmit = (data) => {
    dispatch(createCategory(data))
    .unwrap()
    .then(() => {
      toast.success(SuccessCode.CREATE_CATEGORY_SUCCESS);
      navigate('/dashboard/category', { replace: true });
    })
    .catch((err) => {
      if(ErrorCode[err])
        toast.error(ErrorCode[err]);
      else
        toast.error(ErrorCode.ERR_CREATE_CATEGORY_FAILED);
    })

    // ;
  };

  const editorContent = watch("description");

  const onEditorStateChange = (editorState) => {
    setValue("description", editorState);
  };

  return (
    <Page title="Create category">
      <Container>
        <FormProvider methods={methods} onSubmit={handleSubmit(onSubmit)}>
          <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
            <Typography variant="h4" gutterBottom>
              Create Category
            </Typography>
            <LoadingButton
              type="submit"
              variant="contained"
              loading={isSubmitting}
              startIcon={<Iconify icon="eva:plus-fill" />}
            >
              New Category
            </LoadingButton>
          </Stack>
          <Stack spacing={3}>
            <RHFTextField
              name={"name"}
              label="Category name"
            />

            <FormControl fullWidth>
              <InputLabel id="category-parent-label">Parent</InputLabel>
              <Select
                name='parentId'
                labelId="category-parent-label"
                id="select-category-parent"
                label="Parent"
                {...register("parentId")}
              >
                {console.log(selectCategory)}
                <MenuItem value={null}>uncategory</MenuItem>
                {selectCategory.length > 0 && selectCategory.map((category, index) => (
                  <MenuItem key={index} value={category.id}>{category.name}</MenuItem>
                ))}
              </Select>
            </FormControl>

            <ReactQuill
              theme="snow"
              modules={{
                toolbar: toolbarOptions
              }}
              name="description"
              value={editorContent}
              onChange={onEditorStateChange}
              style={{ height: 300 }}
            />
          </Stack>
        </FormProvider>
      </Container>
    </Page>
  );
}
