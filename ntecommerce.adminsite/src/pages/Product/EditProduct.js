/* eslint-disable no-plusplus */
/* eslint-disable jsx-a11y/label-has-associated-control */
import * as Yup from 'yup';
import { isNaN, replace } from "lodash";
import { styled } from "@mui/system";
import ReactQuill from 'react-quill';
import { toast } from 'react-toastify';
import { LoadingButton } from '@mui/lab';
import { useForm } from "react-hook-form";
import { useEffect, useRef, useState } from "react";
import { yupResolver } from '@hookform/resolvers/yup';
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams, Link as RouterLink } from "react-router-dom";
import { Avatar, Badge, Button, Container, FormControl, ImageList, ImageListItem, InputLabel, MenuItem, Select, Stack, TextField, Typography } from '@mui/material';
import CurrencyFormat from 'react-currency-format';

import Page from '../../components/Page';
import Iconify from '../../components/Iconify';
import { ErrorCode } from '../../constant/ErrorCode';
import categoryService from "../../services/CategoryService";
import { FormProvider, RHFTextField } from '../../components/hook-form';
import { getProduct, removeProduct, updateProduct } from "../../features/productSlice";

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
export default function EditProduct() {
    const navigate = useNavigate();

    const dispatch = useDispatch();

    const { id } = useParams();

    const ref = useRef();

    const { product } = useSelector((state) => state.product);

    const [selectCategory, setSelectCategory] = useState([]);

    const [previewImages, setPreviewImages] = useState([]);

    useEffect(() => {
        categoryService.getListCategories().then((response) => {
            setSelectCategory(response.data);
        });
    }, []);

    useEffect(() => {
        dispatch(getProduct(id))
            .unwrap().then((data) => {
                const { name, category, description, price, images } = data;
                setValue("name", name);
                setValue("categoryId", category && category.id);
                setValue("price", price);
                setValue("description", description);
                setValue('images', []);
                setValue('imageLink', images)
            });
        return () => {
            dispatch(removeProduct());
        }
    }, [dispatch, id]);

    // useEffect(() => {
    //     if (product)
    //     {
    //         setValue('name', product.name)
    //         setValue('categoryId', product.category ? product.category.id : 'uncategory')
    //     }
    // }, [])


    const CreateSchema = Yup.object().shape({
        name: Yup.string().required('Product name is required'),
        price: Yup.number().transform((value) => (isNaN(value) ? undefined : value)).nullable().min(50000, "Min of price is 50,000đ").required("Price is required")
    });

    const methods = useForm({
        resolver: yupResolver(CreateSchema),
        defaultValues: {
            images: null
        }
    });

    const {
        handleSubmit,
        formState: { isSubmitting },
        register,
        watch,
        setValue,
        getValues,

    } = methods;

    const getFormData = (object) => {
        const formData = new FormData();
        Object.keys(object).forEach(key => {
            if (typeof object[key] !== 'object') formData.append(key, object[key]);
            else formData.append(key, JSON.stringify(object[key]));
        });
        return formData;
    };


    const onSubmit = (data) => {
        const body = getFormData(data);

        if (getValues('images') && getValues('images').length > 0) {
            const images = [...getValues('images')];
            for (let index = 0; index < images.length; index++) {
                body.append("Images", images[index]);
            }
        }
        dispatch(updateProduct({id,data: body})).unwrap()
            .then(() => {
                toast.success("Update successfully");
                navigate('/dashboard/products', { replace: true });
            }).catch(err => {
                if (ErrorCode[err])
                    toast.error(ErrorCode[err]);
                else
                    toast.error("Edit failed");
            })
    };

    const editorContent = watch("description");

    const onEditorStateChange = (editorState) => {
        setValue("description", editorState);
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
        <Page title="Edit Product">
            <Container>
                {
                    Object.keys(product).length !== 0 && (
                        <FormProvider methods={methods} onSubmit={handleSubmit(onSubmit)}>
                            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                                <Typography variant="h4" gutterBottom>
                                    Edit Product
                                </Typography>
                                <LoadingButton
                                    type="submit"
                                    variant="contained"
                                    loading={isSubmitting}
                                    startIcon={<Iconify icon="eva:plus-fill" />}
                                >
                                    Edit Product
                                </LoadingButton>
                            </Stack>
                            {console.log(watch())}
                            <Stack spacing={3}>
                                <TextField
                                    label="Product name"
                                    {...register("name")}
                                />
                                <FormControl fullWidth>
                                    <InputLabel id="category-label">Category</InputLabel>
                                    <Select
                                        labelId="category-label"
                                        id="select-category"
                                        label="Category"
                                        defaultValue={product.category && product.category.id}
                                        {...register("categoryId")}
                                    >
                                        {/* {console.log(selectCategory)} */}
                                        <MenuItem value={'uncategory'}>uncategory</MenuItem>
                                        {selectCategory.length > 0 && selectCategory.map((category, index) => (
                                            <MenuItem key={index} value={category.id}>{category.name}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                <CurrencyFormat
                                    customInput={TextField}
                                    thousandSeparator
                                    prefix="đ"
                                    value={getValues('price')}
                                    {...register("price")}
                                    onValueChange={(values) => {
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
                                <ImageList sx={{ width: "100%", height: 500 }} cols={3} rowHeight={200}>
                                    {getValues('imageLink') && getValues('imageLink').length > 0 && getValues('imageLink').map(item => (
                                        <ImageListItem key={item.name} style={{ padding: 5 }}>
                                            <Badge overlap="rectangular" badgeContent="x"
                                                onClick={() => setValue('imageLink', getValues('imageLink').filter(x => x.name !== item.name))}
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
                    )
                }
            </Container>
        </Page>
    )
}