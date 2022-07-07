import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import HTMLEllipsis from 'react-lines-ellipsis/lib/html';
import { useParams, Link as RouterLink } from "react-router-dom";
import { Button, Card, Container, Grid, Stack, Typography } from "@mui/material";

import Page from "../../components/Page";
import Iconify from "../../components/Iconify";
import { getCategory, removeCategory } from "../../features/categorySlice";

export default function CategoryDetail() {
    const { id } = useParams();

    const dispatch = useDispatch();

    const { category } = useSelector((state) => state.category);

    const [useEllipsis, setUseEllipsis] = useState(true);

    useEffect(() => {
        dispatch(getCategory(id));

        return () => {
            dispatch(removeCategory());
        }
    }, [dispatch, id]);

    const handleTextClick = (e) => {
        e.preventDefault()
        setUseEllipsis(!useEllipsis);
    }

    return (
        <Page title="Category">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        Category detail
                    </Typography>
                    <Button variant="contained" color="secondary" component={RouterLink} to="../category" startIcon={<Iconify icon="eva:corner-up-left-fill" />}>
                        Back
                    </Button>
                </Stack>
                {Object.keys(category).length !== 0 &&
                    (
                        <Card>
                            <Grid container style={{ padding: 20, }}>
                                <Grid item xs={6}>
                                    <Stack direction="row" alignItems="center" justifyContent="flex-start" mb={5}>
                                        <Typography variant="subtitle1">
                                            Category name:
                                        </Typography>
                                        <Typography variant="body1" style={{paddingLeft: 10}}>
                                            {category.name}
                                        </Typography>
                                    </Stack>
                                </Grid>
                                <Grid item xs={6}>
                                    <Stack direction="row" alignItems="center" justifyContent="flex-start" mb={5}>
                                        <Typography variant="subtitle1">
                                            Category parent:
                                        </Typography>
                                        <Typography variant="body1" style={{paddingLeft: 10}}>
                                            {category.categoryParent ? category.categoryParent.name : "uncategory"}
                                        </Typography>
                                    </Stack>
                                </Grid>
                                {/* <Grid item xs={1} /> */}
                                <Grid item xs={12} onClick={handleTextClick} >
                                    <Typography variant="subtitle1" style={{paddingBottom: 10}}>
                                        Description:
                                    </Typography>
                                    {category.description && (
                                        useEllipsis ? (
                                            <HTMLEllipsis
                                                // innerRef={node => { this.linesEllipsis = node }}
                                                component='article'
                                                className='ellipsis-html'
                                                unsafeHTML={category.description}
                                                maxLine={3}
                                                ellipsisHTML='<b>... read more</b>'
                                            />

                                        ) : (
                                            <article className='ellipsis-html' dangerouslySetInnerHTML={{ __html: category.description }} />
                                        )
                                    )}
                                </Grid>
                                {/* <Grid item xs={1} /> */}
                            </Grid>
                        </Card>
                    )}
            </Container>
        </Page>
    )
}