import { Button, Card, Container, Grid, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams, Link as RouterLink } from "react-router-dom";
import CurrencyFormat from "react-currency-format";
import HTMLEllipsis from "react-lines-ellipsis/lib/html";
import Carousel from 'react-material-ui-carousel'

import { getProduct, removeProduct } from "../../features/productSlice";
import Iconify from "../../components/Iconify";
import Page from "../../components/Page";

export default function ProductDetail() {
    const { id } = useParams();

    const dispatch = useDispatch();

    const { product } = useSelector((state) => state.product);

    const [useEllipsis, setUseEllipsis] = useState(false);

    useEffect(() => {
        dispatch(getProduct(id));

        return () => {
            dispatch(removeProduct());
        }
    }, [dispatch, id]);

    const handleTextClick = (e) => {
        e.preventDefault()
        setUseEllipsis(!useEllipsis);
    }

    return (
        <Page title="Product detail">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        Product detail
                    </Typography>
                    <Button variant="contained" color="secondary" component={RouterLink} to="../products" startIcon={<Iconify icon="eva:corner-up-left-fill" />}>
                        Back
                    </Button>
                </Stack>
                {Object.keys(product).length !== 0 && (
                    <Card>
                        <Grid container style={{ padding: 20, }}>
                            <Grid item xs={12}>
                                <Carousel autoPlay swipe>
                                    {product.images && product.images.length > 0 && product.images.map((item, i) => (
                                        <img alt="images" src={item} key={i} />
                                    )
                                    )}
                                </Carousel>
                            </Grid>
                            <Grid item xs={6}>
                                <Stack direction="row" alignItems="center" justifyContent="flex-start" mb={5}>
                                    <Typography variant="subtitle1">
                                        Product name:
                                    </Typography>
                                    <Typography variant="body1" style={{ paddingLeft: 10 }}>
                                        {product.name}
                                    </Typography>
                                </Stack>
                            </Grid>
                            <Grid item xs={6}>
                                <Stack direction="row" alignItems="center" justifyContent="flex-start" mb={5}>
                                    <Typography variant="subtitle1">
                                        Category:
                                    </Typography>
                                    <Typography variant="body1" style={{ paddingLeft: 10 }}>
                                        {product.category ? product.category : "uncategory"}
                                    </Typography>
                                </Stack>
                            </Grid>
                            <Grid item xs={6}>
                                <Stack direction="row" alignItems="center" justifyContent="flex-start" mb={5}>
                                    <Typography variant="subtitle1">
                                        Price:
                                    </Typography>
                                    <CurrencyFormat value={product.price} displayType={'text'} thousandSeparator prefix="đ"
                                        renderText={
                                            value =>
                                                <Typography variant="body1" style={{ paddingLeft: 10 }}>
                                                    {value}
                                                </Typography>
                                        } />
                                </Stack>
                            </Grid>
                            <Grid item xs={6} />
                            <Grid item xs={12} onClick={handleTextClick} >
                                <Typography variant="subtitle1" style={{ paddingBottom: 10 }}>
                                    Description:
                                </Typography>
                                {product.description && (
                                    useEllipsis ? (
                                        <HTMLEllipsis
                                            // innerRef={node => { this.linesEllipsis = node }}
                                            component='article'
                                            className='ellipsis-html'
                                            unsafeHTML={product.description}
                                            maxLine={3}
                                            ellipsisHTML='<b>... read more</b>'
                                        />

                                    ) : (
                                        <article className='ellipsis-html' dangerouslySetInnerHTML={{ __html: product.description }} />
                                    )
                                )}
                            </Grid>
                        </Grid>
                    </Card>
                )}
            </Container>
        </Page>
    )
}