/* eslint-disable array-callback-return */
/* eslint-disable no-plusplus */
/* eslint-disable jsx-a11y/label-has-associated-control */
import { useEffect, useRef, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { Avatar, Badge, Button, Container, FormControl, ImageList, ImageListItem, InputLabel, MenuItem, Select, Stack, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { LoadingButton } from "@mui/lab";
import CurrencyFormat from "react-currency-format";
import { isNaN } from "lodash";
import ReactQuill from "react-quill";
import { styled } from "@mui/system";
import { toast } from "react-toastify";

import Page from "../../components/Page";
import categoryService from "../../services/CategoryService";
import { FormProvider, RHFTextField } from "../../components/hook-form";
import Iconify from "../../components/Iconify";
import { createProduct } from "../../features/productSlice";
import { ErrorCode } from "../../constant/ErrorCode";

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

const InputAvatar = styled('input')({
  display: 'none',
});

export default function CreateProduct() {
  const navigate = useNavigate();

  const dispatch = useDispatch();

  const ref = useRef();

  const [selectCategory, setSelectCategory] = useState([]);

  const [previewImages, setPreviewImages] = useState([]);

  useEffect(() => {
    categoryService.getListCategories().then((response) => {
      setSelectCategory(response.data);
    });
  }, []);

  const CreateSchema = Yup.object().shape({
    name: Yup.string().required('Product name is required'),
    price: Yup.number().transform((value) => (isNaN(value) ? undefined : value)).nullable().min(50000, "Min of price is 50,000đ").required("Price is required")
  });

  const methods = useForm({
    resolver: yupResolver(CreateSchema),
    defaultValues: {
      name: '',
      categoryId: null,
      description: '',
      price: '',
      images: null
    },
  });

  const {
    handleSubmit,
    formState: { isSubmitting },
    register,
    watch,
    setValue,
    getValues
  } = methods;

  const editorContent = watch("description");

  const onEditorStateChange = (editorState) => {
    setValue("description", editorState);
  };

  const getFormData = (object) => {
    const formData = new FormData();
    Object.keys(object).forEach(key => {
      if (typeof object[key] !== 'object') formData.append(key, object[key]);
      else formData.append(key, JSON.stringify(object[key]));
    });
    return formData;
  };

  const onSubmit = (data) => {
    console.log(data);
    const body = getFormData(data);
    if (getValues('images') && getValues('images').length > 0) {
      const images = [...getValues('images')];
      for (let index = 0; index < images.length; index++) {
        body.append("Images", images[index]);
      }
    }
    dispatch(createProduct(body)).unwrap()
      .then(() => {
        toast.success("Create successfully");
        navigate('/dashboard/products', { replace: true });
      }).cacth((err) => {
        if (ErrorCode[err])
          toast.error(ErrorCode[err]);
        else
          toast.error("Create failed");
      })
    // dispatch(createCategory(data))
    // .unwrap()
    // .then(() => {
    //   toast.success(SuccessCode.CREATE_CATEGORY_SUCCESS);
    //   navigate('/dashboard/category', { replace: true });
    // })
    // .catch((err) => {
    //   if(ErrorCode[err])
    //     toast.error(ErrorCode[err]);
    //   else
    //     toast.error(ErrorCode.ERR_CREATE_CATEGORY_FAILED);
    // })
    // ;
  };

  const handleRemoveImg = (name) => {
    const listImg = [...getValues('images')];
    const listPreview = [...previewImages];

    const removeImg = listImg.findIndex(item => item.name === name);
    const removePreviewImg = listPreview.findIndex(item => item.name === name);

    if (removeImg !== -1 && removePreviewImg !== -1) {
      listImg.splice(removeImg, 1);
      listPreview.splice(removePreviewImg, 1);
    }

    ref.current.value = null;
    setValue('images', listImg);
    setPreviewImages(listPreview);
  }

  return (
    <Page title="Create product">
      <Container>
        <FormProvider methods={methods} onSubmit={handleSubmit(onSubmit)}>
          <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
            <Typography variant="h4" gutterBottom>
              Create Product
            </Typography>
            <LoadingButton
              type="submit"
              variant="contained"
              loading={isSubmitting}
              startIcon={<Iconify icon="eva:plus-fill" />}
            >
              New Product
            </LoadingButton>
          </Stack>
          <Stack spacing={3}>
            <RHFTextField
              name={"name"}
              label="Product name"
            />
            <FormControl fullWidth>
              <InputLabel id="category-label">Category</InputLabel>
              <Select
                name='categoryId'
                labelId="category-label"
                id="select-category"
                label="Category"
                {...register("categoryId")}
              >
                {console.log(selectCategory)}
                <MenuItem value={null}>uncategory</MenuItem>
                {selectCategory.length > 0 && selectCategory.map((category, index) => (
                  <MenuItem key={index} value={category.id}>{category.name}</MenuItem>
                ))}
              </Select>
            </FormControl>
            <CurrencyFormat customInput={RHFTextField} thousandSeparator prefix="đ" {...register("price")} onValueChange={(values) => {
              const { value } = values;
              setValue("price", value);
            }} />

            <ReactQuill
              theme="snow"
              modules={{
                toolbar: toolbarOptions
              }}
              name="description"
              value={editorContent}
              onChange={onEditorStateChange}
              style={{ height: 300, paddingBottom: 20 }}
              placeholder="describe product here"
            />

            <label htmlFor={"contained-button-file"}>
              <InputAvatar accept="image/*" id="contained-button-file" multiple type="file"
                ref={ref}
                onChange={(e) => {
                  if (e.target.files.length !== 0) {
                    const files = [];
                    const previews = [];
                    for (let index = 0; index < e.target.files.length; index++) {
                      const element = e.target.files[index];
                      files.push(element);
                      previews.push({
                        link: URL.createObjectURL(element),
                        name: element.name
                      });
                    }
                    if (getValues('images') && getValues('images').length > 0) {

                      const listImg = [...getValues('images')];
                      const listPreview = [...previewImages];
                      files.forEach(element => {
                        if (listImg.findIndex((item) => item.name === element.name) === -1)
                          listImg.push(element);

                      });
                      previews.forEach(element => {
                        if (listPreview.findIndex((item) => item.name === element.name) === -1)
                          listPreview.push(element);
                      });

                      setValue('images', listImg);
                      setPreviewImages(listPreview);

                    }
                    else {
                      setValue("images", files);
                      setPreviewImages(previews);
                    }
                    // setFieldValue("previewFile", URL.createObjectURL(e.target.files[0]));
                  }
                }} />
              <Button
                style={{ width: "100%", marginTop: "10px" }}
                variant="contained"
                component="span"
                startIcon={<Iconify icon="eva:upload-fill" />}
              >
                Upload
              </Button>
            </label>
            {console.log(getValues('images'))}
            {console.log(previewImages)}
            <ImageList sx={{ width: "100%", height: 500 }} cols={3} rowHeight={200}>
              {previewImages.length > 0 && previewImages.map(item => (
                <ImageListItem key={item.name} style={{ padding: 5 }}>
                  <Badge overlap="rectangular" badgeContent="x"
                    onClick={() => handleRemoveImg(item.name)}
                    color="primary" sx={{ cursor: "pointer", width: "fit-content" }}>
                    <Avatar alt={item.name}
                      src={item.link}
                      variant="square"
                      sx={{ width: "100%", height: 200 }}
                      className="avatar" />
                  </Badge>
                </ImageListItem>
              ))
              }
            </ImageList>


          </Stack>
        </FormProvider>
      </Container>
    </Page>
  )
}